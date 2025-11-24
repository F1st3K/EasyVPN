import { ZsvVersion } from '../enums/ZsvVersion';
import ConnectionString from './ConnectionString';

export default interface ServerInfo {
    protocolId: string;
    version: ZsvVersion;
    connection: ConnectionString;
}
