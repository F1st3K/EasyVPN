import Page from '../requests/Page';

export default interface PageWithDates extends Page {
    lastModified: string;
    created: string;
}
