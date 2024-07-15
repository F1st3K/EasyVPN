import { ConnectionTicketStatus } from '../enums/ConnectionTicketStatus';
import User from './User';

export default interface ConnectionTicket {
    id: string;
    connectionId: string;
    client: User;
    status: ConnectionTicketStatus;
    creationTime: Date;
    days: number;
    paymentDescription: string;
    images: string[];
}
