import PaymentConnectionInfo from './PaymentConnectionInfo';

export default interface ExtendConnection extends PaymentConnectionInfo {
    connectionId: string;
}
