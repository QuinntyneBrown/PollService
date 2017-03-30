import { Question } from "../models";
import { EditorComponent } from "../shared";

const template = require("./question.component.html");
const styles = require("./question.component.scss");

export class QuestionComponent extends HTMLElement {
    constructor() {
        super();
    }

    static get observedAttributes () {
        return [
            "question"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.responseBodyEditor = new EditorComponent(this.responseBodyElement);
        this.questionBodyElement.innerHTML = this.question.body;
        this.questionDescriptionElement.innerHTML = this.question.description;
        
    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "question":
                this.question = JSON.parse(newValue);
                break;
        }
    }

    public responseBodyEditor: EditorComponent;

    public get value(): any { return this.responseBodyEditor.text; }

    public get questionId(): any { return this.question.id; }

    public get responseBodyElement(): HTMLElement { return this.querySelector(".response-body") as HTMLElement; }
    public get questionBodyElement(): HTMLElement { return this.querySelector(".question-body") as HTMLElement; }
    public get questionDescriptionElement(): HTMLElement { return this.querySelector(".question-description") as HTMLElement; }
    public question: Question;
}

customElements.define(`ce-question`,QuestionComponent);
