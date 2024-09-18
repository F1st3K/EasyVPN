import PaymentConnectionInfo from '../common/PaymentConnectionInfo';

export default interface CreateConnection extends PaymentConnectionInfo {
    serverId: string;
}
