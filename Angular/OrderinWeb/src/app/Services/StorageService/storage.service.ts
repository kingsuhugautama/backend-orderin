declare var Swal:any;
import { secret_key_localstorage,secret_key_jwt,secret_key_route } from '../../Utility/Url/url-constant';
import { environment } from '../../../environments/environment.prod';

var SECRET_KEY = secret_key_localstorage; //untuk localstorage / cookie
var SECRET_KEY_JWT = secret_key_jwt; //untuk jwt
var SECRET_KEY_ROUTE = secret_key_route; //untuk routing data

import { Injectable } from '@angular/core';

const CryptoJS = require('crypto-js');
const SecureStorage = require('secure-web-storage');

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }

  public secureStorage = new SecureStorage(localStorage, {
    hash: function hash(key) {
      if(environment.isEncriptionStorage)
      key = CryptoJS.SHA256(key, SECRET_KEY);

      return key.toString();
    },
    // Encrypt the localstorage data
    encrypt: function encrypt(data) {
      if(environment.isEncriptionStorage){
        data = CryptoJS.AES.encrypt(data, SECRET_KEY);
        data = data.toString();
      }

      return data;
    },
    // Decrypt the encrypted data
    decrypt: function decrypt(data) {

      if(environment.isEncriptionStorage){
        data = CryptoJS.AES.decrypt(data, SECRET_KEY);
        data = data.toString(CryptoJS.enc.Utf8);
      }

      return data;
    }
  });

  //storage
  setItem(key: string, value: any) {
    this.secureStorage.setItem(key, value);
  }

  // Get the json value from local storage
  getItem(key: string) {
    return this.secureStorage.getItem(key);
  }

  removeItem(key:string){
    return this.secureStorage.removeItem(key);
  }

  // Clear the local storage
  clear() {
    return this.secureStorage.clear();
  }

  encryptSHA1(data) {
    data = CryptoJS.SHA1(data + "mat_orderin_user_data");
    data = data.toString();
    return data;
  }

  customAESEncrypt(data,secret_key){
    return CryptoJS.AES.encrypt(data,secret_key).toString();
  }

  customAESDecrypt(data,secret_key){
    return CryptoJS.AES.decrypt(data,secret_key).toString(CryptoJS.enc.Utf8);
  }

  encrypt(data,key = "") {
    if(key == "") key = SECRET_KEY_JWT;

    data = CryptoJS.AES.encrypt(data,key);
    data = data.toString();
    return data;
  }

  decrypt(data,key = "") {
    if(key == "") key = SECRET_KEY_JWT;

    data = CryptoJS.AES.decrypt(data,key);
    data = data.toString(CryptoJS.enc.Utf8);
    return data;
  }
}
