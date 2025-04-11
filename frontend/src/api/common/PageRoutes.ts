import PageInfo from '../responses/PageInfo';

export type PageRoutes = {
    page: PageInfo;
    childrens: PageRoutes[];
};
