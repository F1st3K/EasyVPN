import { Role } from "./Roles"
import User from "./User"

export default interface AuthResponse extends User {
    token: string
    roles: Role[]
}