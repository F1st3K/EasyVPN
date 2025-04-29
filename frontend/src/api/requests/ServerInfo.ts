import { VpnVersion } from '../enums/VpnVersion';
import ConnectionString from './Connection';

export default interface ServerInfo {
    protocolId: string;
    version: VpnVersion;
    connection: ConnectionString;
}
