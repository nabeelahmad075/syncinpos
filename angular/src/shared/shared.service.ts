import { Injectable } from '@angular/core';
import { v4 as uuidV4 } from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() {
    
  }
  getUniqueId():string {
    return uuidV4();
  }
}
