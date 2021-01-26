import { inject } from 'aurelia-framework';
import { I18N } from 'aurelia-i18n';
import swal from 'sweetalert';

@inject(I18N)
export class NotificationService {

  constructor(private i18n: I18N) {
    console.log(i18n.getLocale());
  }

  success(title: string, message: string): Promise<any> {
    if(title == null)
      title = this.i18n.tr('Success!');
    return swal(title, message, 'success');
  }

  alert(title: string, message: string = null): Promise<any> {
    if(message != null)
      return swal(title, message, 'warning');
    return swal(title, '', 'warning');
  }

  danger(title: string, message: string = null): Promise<any> {
    if(message != null)
      return swal(title, message, 'danger');
    return swal(title, '', 'danger');
  }

  confirm(): Promise<boolean> {

    let yes: boolean;
    let no: boolean;

    return swal({
      title: this.i18n.tr('Are you Sure'),
      icon: 'warning',
      buttons: [ this.i18n.tr('no'), this.i18n.tr('yes')]
    })
      .then((value) => {
        if (value) {
          return true;
        } else {
          return false;
        }
      }).catch(e => {
        console.log(e);
        return false;
      });
  }
}
