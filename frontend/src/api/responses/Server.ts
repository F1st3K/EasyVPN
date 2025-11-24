import { ZsvVersion } from '../enums/ZsvVersion';
import Protocol from './Protocol';

export default interface Server {
    id: string;
    protocol: Protocol;
    version: ZsvVersion;
}
