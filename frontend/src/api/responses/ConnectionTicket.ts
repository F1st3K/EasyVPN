import PaymentConnectionInfo from '../common/PaymentConnectionInfo';
import { ConnectionTicketStatus } from '../enums/ConnectionTicketStatus';
import User from './User';

export default interface ConnectionTicket extends PaymentConnectionInfo {
    id: string;
    connectionId: string;
    client: User;
    status: ConnectionTicketStatus;
    creationTime: Date;
}
