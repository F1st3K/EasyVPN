import { makeAutoObservable } from 'mobx';

import EasyVpn from '../api';
import { PageRoutes } from '../api/common/PageRoutes';
import PageInfo from '../api/responses/PageInfo';

export default class PageStore {
    routes = null as PageRoutes[] | null;

    constructor() {
        this.sync();
        makeAutoObservable(this);
    }

    public getPageRoutes(): PageRoutes[] {
        if (this.routes === null) this.sync();
        return this.routes ?? [];
    }

    public async sync() {
        this.routes = this.buildRoutes(await EasyVpn.pages.getAll().then((r) => r.data));
    }

    private buildRoutes(pages: PageInfo[]): PageRoutes[] {
        const result: PageRoutes[] = [];

        for (const page of pages.sort((a, b) => a.route.length - b.route.length)) {
            const parts = page.route.split('/');
            let currentLevel = result;

            for (const part of parts) {
                // Найдем существующий маршрут или создадим новый
                let existingRoute = currentLevel.find(
                    (route) => route.page.route === part,
                );

                if (!existingRoute) {
                    existingRoute = {
                        page: { ...page, route: part }, // Сохраняем информацию о странице
                        childrens: [],
                    };
                    currentLevel.push(existingRoute);
                }
                currentLevel = existingRoute.childrens;
            }
        }

        return result;
    }
}
