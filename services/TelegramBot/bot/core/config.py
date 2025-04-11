from __future__ import annotations
from pathlib import Path

from pydantic_settings import BaseSettings, SettingsConfigDict

DIR = Path(__file__).absolute().parent.parent.parent
BOT_DIR = Path(__file__).absolute().parent.parent
LOCALES_DIR = f"{BOT_DIR}/locales"
I18N_DOMAIN = "messages"
DEFAULT_LOCALE = "en"

class EnvBaseSettings(BaseSettings):
    model_config = SettingsConfigDict(env_file=".env", env_file_encoding="utf-8", extra="ignore")


class BotSettings(EnvBaseSettings):
    BOT_TOKEN: str


class DBSettings(EnvBaseSettings):
    DB_PATH: str = "tgbot.db"

    DB_DIR: str = str(BOT_DIR / "data")

    @property
    def database_url(self) -> str:
        db_file_path = Path(self.DB_DIR) / self.DB_PATH
        
        db_file_path.parent.mkdir(parents=True, exist_ok=True)
        return f"sqlite+aiosqlite:///{db_file_path}"


class Settings(BotSettings, DBSettings):
    DEBUG: bool = False

settings = Settings()
