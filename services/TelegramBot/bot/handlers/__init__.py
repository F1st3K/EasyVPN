from aiogram import Router


def get_handlers_router() -> Router:
    from . import start, help

    router = Router()
    router.include_router(start.router)
    router.include_router(help.router)

    return router
