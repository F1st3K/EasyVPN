import { Role } from '../enums/Role';

export default interface User {
    id: string;
    firstName: string;
    lastName: string;
    icon: string;
    login: string;
    roles: Role[];
}
