import Server from './Server';
import User from './User';

export default interface Connection {
    id: string;
    client: User;
    server: Server;
    validUntil: string;
}
