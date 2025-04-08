from __future__ import annotations
from typing import TYPE_CHECKING

from sqlalchemy import func, select, update, exc
from sqlalchemy.dialects.sqlite import insert

from bot.database.models import UserModel

if TYPE_CHECKING:
    from aiogram.types import User
    from sqlalchemy.ext.asyncio import AsyncSession


async def create_user(
    session: AsyncSession,
    user: User,
) -> None:
    """Insert or update a user by Telegram ID."""
    stmt = insert(UserModel).values(
        id=user.id,
        language_code=user.language_code,
    ).on_conflict_do_update(
        index_elements=["id"],
        set_={
            "language_code": user.language_code,
        },
    )

    try:
        await session.execute(stmt)
        await session.commit()
    except exc.SQLAlchemyError as e:
        await session.rollback()
        raise e


async def get_language_code(session: AsyncSession, user_id: int) -> str:
    """Get user's language code from the database."""
    query = select(UserModel.language_code).filter_by(id=user_id)

    result = await session.execute(query)
    language_code = result.scalar_one_or_none()
    return language_code or ""


async def set_language_code(
    session: AsyncSession,
    user_id: int,
    language_code: str,
) -> None:
    """Set user's language code in the database."""
    stmt = update(UserModel).where(UserModel.id == user_id).values(language_code=language_code)

    await session.execute(stmt)
    await session.commit()


async def get_all_users(session: AsyncSession) -> list[UserModel]:
    """Get all users from the database."""
    query = select(UserModel)

    result = await session.execute(query)
    users = result.scalars()
    return list(users)


async def get_user_count(session: AsyncSession) -> int:
    """Get total count of users in the database."""
    query = select(func.count()).select_from(UserModel)

    result = await session.execute(query)
    count = result.scalar_one_or_none() or 0
    return int(count)
