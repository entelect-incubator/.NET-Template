/**
 * .NET Vertical Slice Minimum Architecture API
 *
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { ResultOfPizzaModelData } from './resultOfPizzaModelData';


export interface ResultOfPizzaModel { 
    IsError?: boolean;
    IsValidationError?: boolean;
    Data?: ResultOfPizzaModelData | null;
    Count?: number;
    Message?: string;
    Errors?: Array<string>;
    ValidationErrors?: { [key: string]: Array<string>; };
}

