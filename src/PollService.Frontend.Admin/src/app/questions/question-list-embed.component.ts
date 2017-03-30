import { Question } from "./question.model";

const template = require("./question-list-embed.component.html");
const styles = require("./question-list-embed.component.scss");

export class QuestionListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "questions"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.questions.length; i++) {
            let el = this._document.createElement(`ce-question-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.questions[i]));
            this.appendChild(el);
        }    
    }

    questions:Array<Question> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "questions":
                this.questions = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-question-list-embed", QuestionListEmbedComponent);
