from aiogram import Router, types
from aiogram.filters import Command
from aiogram.utils.i18n import gettext as _

from bot.keyboards.inline.menu import main_keyboard

router = Router(name="help")


@router.message(Command("help"))
async def help_handler(message: types.Message) -> None:
    """Help message with available commands."""
    await message.answer(_("help message"), reply_markup=main_keyboard())