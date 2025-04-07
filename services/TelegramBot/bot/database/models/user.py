from __future__ import annotations
import datetime

from sqlalchemy import BigInteger
from sqlalchemy.orm import Mapped, mapped_column
from sqlalchemy.sql import func

from bot.database.models.base import Base


class UserModel(Base):
    __tablename__ = "users"

    id: Mapped[int] = mapped_column(BigInteger, primary_key=True, autoincrement=False)
    language_code: Mapped[str | None]
    created_at: Mapped[datetime.datetime] = mapped_column(server_default=func.now())
