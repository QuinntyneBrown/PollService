import { fetch } from "../utilities";
import { Question, Poll } from "../models";
import { environment } from "../environment";

export class ApiService {
    constructor(private _fetch = fetch) { }

    private static _instance: ApiService;

    public static get Instance() {
        this._instance = this._instance || new this();
        return this._instance;
    }

    public getPoll(): Promise<Poll> {        
        return this._fetch({ url: `/api/poll/getbyuniqueId?uniqueId=${environment.pollUniqueId}`, authRequired: false }).then((results: string) => {
            return (JSON.parse(results) as { survey: Poll }).survey;
        });
    }   

    public addPollResult(pollResult): Promise<any> {
        return this._fetch({ url: `/api/pollresult/add`, authRequired: false, method: "POST", data: { pollResult, surveyUniqueId:environment.pollUniqueId } }).then((results: string) => {
            return true;
        });
    } 
}
