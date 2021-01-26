import { inject } from 'aurelia-framework';

@inject(Element)
export class Dialog {
  constructor(public element: Element) {}

  close(): void {
    const event = new CustomEvent('close');
    this.element.dispatchEvent(event);
  }
}
