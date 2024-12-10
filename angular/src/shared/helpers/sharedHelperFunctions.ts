
export class sharedHelperFunctions{
  public static leadingZeros = (givenNumber: number, places: number):string => {
    if (sharedHelperFunctions.isNullOrUndefined(givenNumber))
      return '';
    if (!places)
      return givenNumber.toString();
    let leadingZeros = '';
    for (let index = 0; index < places-givenNumber.toString().length; index++) {
      leadingZeros += '0';
    }
    return leadingZeros + givenNumber;
  };
  public static isNullOrUndefined = (givenValue: any): boolean => (givenValue == undefined || givenValue == null);
  public static getSum(col: string,arr:any[]) {
    return arr.reduce((sum, current) => sum + parseFloat((current[col]) ?? 0), 0);
  }
}