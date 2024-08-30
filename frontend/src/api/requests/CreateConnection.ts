import PaymentConnectionInfo from './PaymentConnectionInfo';

export default interface CreateConnection extends PaymentConnectionInfo {
    serverId: string;
}
