import { VpnVersion } from '../enums/VpnVersion';
import ConnectionString from './ConnectionString';

export default interface ServerInfo {
    protocolId: string;
    version: VpnVersion;
    connection: ConnectionString;
}
