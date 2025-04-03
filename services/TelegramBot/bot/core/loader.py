from aiogram import Bot, Dispatcher
from aiogram.client.default import DefaultBotProperties
from aiogram.enums import ParseMode

from bot.core.config import settings

token = settings.BOT_TOKEN

bot = Bot(token=token, default=DefaultBotProperties(parse_mode=ParseMode.HTML))

dp = Dispatcher()

DEBUG = settings.DEBUG
