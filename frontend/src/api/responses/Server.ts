import { VpnVersion } from '../enums/VpnVersion';
import Protocol from './Protocol';

export default interface Server {
    id: string;
    protocol: Protocol;
    version: VpnVersion;
}
