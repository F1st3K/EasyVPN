from __future__ import annotations
from typing import TYPE_CHECKING, Any

from aiogram import BaseMiddleware
from aiogram.types import TelegramObject, User, Update
from loguru import logger

from bot.services.users import create_user

if TYPE_CHECKING:
    from collections.abc import Awaitable, Callable
    from sqlalchemy.ext.asyncio import AsyncSession

class AuthMiddleware(BaseMiddleware):
    EVENT_FIELDS = [
        "message",
        # "callback_query",
        # "inline_query",
        # "chat_member",
        # "my_chat_member",
        # "chat_join_request",
    ]

    async def __call__(
        self,
        handler: Callable[[TelegramObject, dict[str, Any]], Awaitable[Any]],
        event: TelegramObject,
        data: dict[str, Any],
    ) -> Any:
        session: AsyncSession = data["session"]

        if not isinstance(event, Update):
            return await handler(event, data)

        user: User | None = None

        for field in self.EVENT_FIELDS:
            event_field = getattr(event, field, None)
            if event_field:
                user = getattr(event_field, "from_user", None)
                
                break

        if not user:
            logger.debug(f"No user found in event: {event.__class__.__name__}")
            return await handler(event, data)

        logger.info(f"new user registration | user_id: {user.id} | event: {event.__class__.__name__}")

        await create_user(session=session, user=user)

        return await handler(event, data)
    