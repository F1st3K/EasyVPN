from aiogram.types import InlineKeyboardButton, InlineKeyboardMarkup
from aiogram.utils.i18n import gettext as _
from aiogram.utils.keyboard import InlineKeyboardBuilder


def main_keyboard() -> InlineKeyboardMarkup:
    """Use in main menu."""
    buttons = [
        [InlineKeyboardButton(text=_("info_button"), callback_data="info")],
    ]

    keyboard = InlineKeyboardBuilder(markup=buttons)

    keyboard.adjust(1, 1, 2)

    return keyboard.as_markup()