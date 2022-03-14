
declare var moment,Swal:any;
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HelperService {

  letters = '0123456789ABCDEF';
  public upperCaseForm:boolean = true;

  constructor() {
    moment.locale('id');
  }


  getRandomColor() {
    let color = '#';
    for (var i = 0; i < 6; i++) {
      color += this.letters[Math.floor(Math.random() * 16)];
    }

    return color;
  }

  getDateTimeYMD() {
    let todayDate = moment(new Date().toString()).format("YYYY-MM-DD HH:mm:ss");
    return todayDate;
  }

  getDateYMD() {
    let todayDate = moment(new Date().toString()).format("YYYY-MM-DD");
    return todayDate;
  }

  //mendapatkan tanggal pertama dari bulan
  getFirstDateOfMonth(date){
    var firstDay = moment(date).format("YYYY-MM-01 00:00:00");
    return firstDay;
  }

  //mendapatkan tgl terakhir dari bulan
  getLastDateOfMonth(date){
    var lastDay = moment(date).format("YYYY-MM-") + moment().daysInMonth() + ' 23:59:59';
    return lastDay;
  }

  getCurrentDateISO(){
    var datetime = moment(new Date().toString()).format("YYYY-MM-DD HH:mm:ss").replace(" ","T");
    return datetime;
  }

  ddMMMMyyyy(value) {
    var newdateddMMMMyyyy = moment(this.reformatingDateValue(value));

    return newdateddMMMMyyyy.format("DD MMMM YYYY");
  }

  reformatingYMD(value){
    var newDate = moment(value).format("YYYY-MM-DD");
    return newDate;
  }


  reformatingDatetoISO(date){
    var datetime = date.replace(" ","T");
    return datetime;
  }
  reformatingDateValue(dateValue) {
    //console.log(dateValue);
    let tgl = dateValue;
    let myDate = new Date(tgl);
    let d = <any>myDate.getDate();
    let m = <any>myDate.getMonth();
    m += 1;
    if (m < 10) {
      m = '0' + m;
    }
    if (d < 10) {
      d = '0' + d;
    }
    var y = myDate.getFullYear();


    var newdate = "";

    if (dateValue && dateValue != "NaN-NaN-NaN") {
      newdate = (y + "-" + m + "-" + d);
    } else if (dateValue == "NaN-NaN-NaN") {
      newdate = "1900-01-01";
    } else {
      newdate = "1900-01-01";
    }
    //console.log(newdate);
    return newdate;
  }

  swalMsg(msg = '', title = 'Perhatian', icon = 'error') {
    Swal.fire({
      title: title,
      icon: icon,
      text: msg
    });
  }

  swalConfirmation(msg='',title='Perhatian',icon='warning',actionYes:()=>void,actionNo:()=>void){
    Swal.fire({
      title: title,
      text:msg,
      showDenyButton: true,
      showCancelButton: false,
      confirmButtonText: '<i class="fas fa-check"></i> Ya',
      denyButtonText: '<i class="fas fa-times"></i> Tidak',
    }).then((result) => {
      /* Read more about isConfirmed, isDenied below */
      if (result.isConfirmed) {
        actionYes();
      } else if (result.isDenied) {
        actionNo();
      }
    })
  }

  showModal(modal) {
    modal.appendTo("body");
    modal.modal("show");
  }

  formatRupiah(angka):string {
    angka = angka.toString().replace(".", ",");
    //alert(angka);
    let number_string = angka.replace(/[^,\d]/g, '').toString(),
    split = number_string.split(','),
    sisa = split[0].length % 3,
    rupiah = split[0].substr(0, sisa),
    ribuan = split[0].substr(sisa).match(/\d{3}/gi);
    // tambahkan titik jika yang di input sudah menjadi angka ribuan
    if (ribuan) {
      let separator = sisa ? '.' : '';
      rupiah += separator + ribuan.join('.');
    }
    return rupiah;
  }

  // formatRupiah(angka, prefix) {
  //   angka = angka.toString().replace(".", ",");
  //   //alert(angka);
  //   let number_string = angka.replace(/[^,\d]/g, '').toString(),
  //   split = number_string.split(','),
  //   sisa = split[0].length % 3,
  //   rupiah = split[0].substr(0, sisa),
  //   ribuan = split[0].substr(sisa).match(/\d{3}/gi);
  //   // tambahkan titik jika yang di input sudah menjadi angka ribuan
  //   if (ribuan) {
  //     let separator = sisa ? '.' : '';
  //     rupiah += separator + ribuan.join('.');
  //   }
  // }

  replaceEmptyJsonArrayWithNulls(json){
    return JSON.parse(JSON.stringify(json).replace(/\[]+/g,null));
  }

  replaceJsonPropertyName(json,name,replaceWith){
    var replace = name;
    var re = new RegExp(replace,"g");
    return JSON.stringify(json).replace(re,replaceWith);
  }

  //merubah data undefined menjadi null,
  // dan merubah empty value menjadi integer (masukkan ke parameter column)
  updateJsonFormatDataType(data,column:any = []){
    if(column.length > 0){
      for(let item in column){
        // console.log(item);
        data[column[item]] = parseInt(data[column[item]] == undefined ? 0:data[column[item]]);
      }
    }


    for(let index in data){
      if(data[index] == undefined){
        data[index] = null;
      }

      let boolean = data[index] == 'true'? true: data[index] == 'false' ? false: data[index];

      if(typeof(boolean)){
        data[index] = boolean;
      }
    }

    return data;
  }


  leadingString(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
  }

  isHTML(str) {
    var a = document.createElement('div');
    a.innerHTML = str;

    for (var c = a.childNodes, i = c.length; i--; ) {
      if (c[i].nodeType == 1) return true;
    }

    return false;
  }

  removeTagsHTML(str) {
    if ((str===null) || (str===''))
    return str;
    else{
      let strString = str.toString();

      if(this.isHTML(str))
      return strString.replace( /(<([^>]+)>)/ig, '');
      else
      return str
    }
  }

  getAge(dateString) {

    //format harus DD/MM/YYYY = 20/02/2020
    dateString = moment(dateString).format("DD/MM/YYYY");
    let now = new Date();
    let today = new Date(now.getFullYear(), now.getMonth(), now.getDate());

    let yearNow = now.getFullYear();
    let monthNow = now.getMonth();
    let dateNow = now.getDate();

    let dob = new Date(dateString.substring(6, 10),
    dateString.substring(3, 5) - 1,
    dateString.substring(0, 2)
    );

    let yearDob = dob.getFullYear();
    let monthDob = dob.getMonth();
    let dateDob = dob.getDate();
    let age:any = {};
    let ageString = "";
    let yearString = "";
    let monthString = "";
    let dayString = "";

    let monthAge = 0;
    let dateAge = 0;


    let yearAge = yearNow - yearDob;

    if (monthNow >= monthDob)
    monthAge = monthNow - monthDob;
    else {
      yearAge--;
      monthAge = 12 + monthNow - monthDob;
    }

    if (dateNow >= dateDob)
    dateAge = dateNow - dateDob;
    else {
      monthAge--;
      dateAge = 31 + dateNow - dateDob;

      if (monthAge < 0) {
        monthAge = 11;
        yearAge--;
      }
    }

    age = {
      years: yearAge,
      months: monthAge,
      days: dateAge
    };

    // console.log(age)

    if (age.years > 1) yearString = " years";
    else yearString = " year";
    if (age.months > 1) monthString = " months";
    else monthString = " month";
    if (age.days > 1) dayString = " days";
    else dayString = " day";


    if ((age.years > 0) && (age.months > 0) && (age.days > 0))
    ageString = age.years + yearString + ", " + age.months + monthString + " " + age.days + dayString + " ";
    else if ((age.years == 0) && (age.months == 0) && (age.days > 0))
    ageString = " " + age.days + dayString + " ";
    else if ((age.years > 0) && (age.months == 0) && (age.days == 0))
    ageString = age.years + yearString + " ";
    else if ((age.years > 0) && (age.months > 0) && (age.days == 0))
    ageString = age.years + yearString + "  " + age.months + monthString + " ";
    else if ((age.years == 0) && (age.months > 0) && (age.days > 0))
    ageString = age.months + monthString + " " + age.days + dayString + " ";
    else if ((age.years > 0) && (age.months == 0) && (age.days > 0))
    ageString = age.years + yearString + " " + age.days + dayString + " ";
    else if ((age.years == 0) && (age.months > 0) && (age.days == 0))
    ageString = age.months + monthString + " ";
    else ageString = "Oops! Could not calculate age!";

    return age;
  }
}
