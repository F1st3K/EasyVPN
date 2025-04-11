from __future__ import annotations
from typing import TYPE_CHECKING

from sqlalchemy.ext.asyncio import AsyncEngine, AsyncSession, async_sessionmaker, create_async_engine

from bot.core.config import settings
from loguru import logger

if TYPE_CHECKING:
    from sqlalchemy.engine.url import URL


def get_engine(url: URL | str = settings.database_url) -> AsyncEngine:
    return create_async_engine(
        url=url,
        echo=settings.DEBUG,
        pool_size=0, 
    )


def get_sessionmaker(engine: AsyncEngine) -> async_sessionmaker[AsyncSession]:
    return async_sessionmaker(bind=engine, autoflush=False, expire_on_commit=False)


async def create_tables(engine: AsyncEngine) -> None:
    try:
        from bot.database.models import Base
        logger.info("Checking and creating database tables if needed...")
        async with engine.begin() as conn:
            existing_tables = await conn.run_sync(
                lambda sync_conn: sync_conn.dialect.get_table_names(sync_conn)
            )
            logger.info(f"Existing tables: {existing_tables}")
            
            await conn.run_sync(Base.metadata.create_all)
            
            new_tables = await conn.run_sync(
                lambda sync_conn: sync_conn.dialect.get_table_names(sync_conn)
            )
            logger.info(f"Tables after creation: {new_tables}")
            if set(new_tables) != set(existing_tables):
                logger.info("New tables were created")
            else:
                logger.info("No new tables needed to be created")
    except Exception as e:
        logger.error(f"Failed to create tables: {e}")
        raise


engine = get_engine()
sessionmaker = get_sessionmaker(engine)
