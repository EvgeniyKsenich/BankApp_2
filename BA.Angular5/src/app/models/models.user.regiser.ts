export class UserRegister {
    Id: number;
    UserName: string;
    Password: string;
    Name: string;
    Surname: string;
    Email: string;
    DateOfBirth: Date;

    constructor() {
        this.UserName = "";
        this.Password = "";
        this.Name = "";
        this.Surname = "";
        this.Email = "";
        this.DateOfBirth;
    }
}