import PageInfo from '../responses/PageInfo';

export default interface Page extends PageInfo {
    base64Content: string;
}
