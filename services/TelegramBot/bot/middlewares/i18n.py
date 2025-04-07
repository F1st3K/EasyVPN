"""1. Get all texts
pybabel extract --input-dirs=. -o bot/locales/messages.pot --project=messages.

2. Init translations
pybabel init -i bot/locales/messages.pot -d bot/locales -D messages -l en
pybabel init -i bot/locales/messages.pot -d bot/locales -D messages -l ru

3. Compile translations
pybabel compile -d bot/locales -D messages --statistics

pybabel update -i bot/locales/messages.pot -d bot/locales -D messages

"""

from __future__ import annotations
from typing import TYPE_CHECKING, Any

from aiogram.utils.i18n.middleware import I18nMiddleware

from bot.core.config import DEFAULT_LOCALE

if TYPE_CHECKING:
    from aiogram.types import TelegramObject


class ACLMiddleware(I18nMiddleware):
    DEFAULT_LANGUAGE_CODE = DEFAULT_LOCALE

    async def get_locale(self, event: TelegramObject, data: dict[str, Any]) -> str:

        return self.DEFAULT_LANGUAGE_CODE
    