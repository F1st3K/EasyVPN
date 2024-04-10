import { Role } from "./Roles"
import User from "./User"

export default interface Auth extends User {
    token: string
    roles: Role[]
}