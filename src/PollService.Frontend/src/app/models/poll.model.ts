export class Poll { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Poll {
        let poll = new Poll();
        poll.name = data.name;
        return poll;
    }
}
