export class User{
    Id:number;
    UserName:string;
    Name:string;
    Surname:string;
    Balance:number;

    constructor(){
        this.Id = -1;
        this.Balance = 0;
        this.UserName = "Username";
    }
}