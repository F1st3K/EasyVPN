import PaymentConnectionInfo from '../common/PaymentConnectionInfo';

export default interface ExtendConnection extends PaymentConnectionInfo {
    connectionId: string;
}
