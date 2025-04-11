import AuthStore from './AuthStore';
import PageStore from './PageStore';

export default interface Store {
    Auth: AuthStore;
    Pages: PageStore;
}

export { AuthStore, PageStore };
