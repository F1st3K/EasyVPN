import Protocol from './Protocol';
import { VpnVersion } from '../enums/VpnVersion';

export default interface Server {
    id: string;
    protocol: Protocol;
    version: VpnVersion;
}
