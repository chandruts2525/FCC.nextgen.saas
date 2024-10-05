const alphaNumericValidationRegexWithSpace = /^[a-zA-Z0-9 ]*$/;
const alphaNumericValidationRegex = /^[a-zA-Z0-9]*$/;
const websiteURLRegex =
  /^((https?|http?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$/;
const emailValidationRegex =
  /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

//types
const REQUIRED = 'required';
const EMAIL = 'email';
const REQUIREDANDEMAIL = 'required&email';
const REQUIREDANDROLENAME = 'required&rolename';
const MAXIMUM = 'maximum';
const NUMERIC = 'numeric';
const REQUIREDANDNUMERIC = 'required&numeric';
const URL = 'url';
const REQUIREDANDURL = 'required&url';

export function checkValidationError(
  type:
    | 'required'
    | 'email'
    | 'required&email'
    | 'required&rolename'
    | 'maximum'
    | 'numeric'
    | 'required&numeric'
    | 'required&url'
    | 'url',
  val: any,
  field?: 'dropdown' | undefined,
  message: any = null
) {
  switch (type) {
    case REQUIRED:
      return validateRequired(val, field, message);
    case EMAIL:
      return validateEmail(val, message);
    case REQUIREDANDEMAIL:
      return validateRequired(val, field, message) ?? validateEmail(val, message);
    case REQUIREDANDROLENAME:
      return validateRequired(val, message) ?? roleNameRegex(val, message);
    case MAXIMUM:
      return validateMaximum(val);
    case NUMERIC:
      return validateNumeric(val, message);
    case REQUIREDANDNUMERIC:
      return validateRequired(val) ?? validateNumeric(val);
    case REQUIREDANDURL:
      return validateRequired(val, field, message) ?? validateURL(val, message);
    case URL:
      return validateURL(val, message);
  }
  // if (type === 'required') {
  //   return validateRequired(val, field, message);
  // } else if (type === 'email') {
  //   return validateEmail(val, message);
  // } else if (type === 'required&email') {
  //   return validateRequired(val, field, message) ?? validateEmail(val, message);
  // } else if (type === 'required&rolename') {
  //   return validateRequired(val, message) ?? roleNameRegex(val, message);
  // } else if (type === 'maximum') {
  //   return validateMaximum(val);
  // } else if (type === 'required&numeric') {
  //   return validateRequired(val) ?? validateNumeric(val);
  // } else if (type === 'numeric') {
  //   return validateNumeric(val, message);
  // }
}

export function validateNumeric(val: any, message?: any) {
  if (val) {
    return val % 1 === 0 && val?.toString()?.length <= 4
      ? null
      : {
          errorMessage: message || 'Only numeric characters are allowed (Maximum 4).',
        };
  } else return null;
}

export function validateMaximum(val: any, message?: any) {
  if (val) {
    return val % 1 === 0 && val?.toString()?.length <= 5
      ? null
      : {
          errorMessage: message || 'Only numeric characters are allowed (Maximum 5).',
        };
  } else return null;
}

// email validation==============================
export function validateEmail(val: any, message?: any) {
  if (val === undefined) return null;
  else if (val.length > 0) {
    return checkEmailValidation(val)
      ? null
      : { errorMessage: 'This email id is not valid' };
  } else return { errorMessage: message || 'This field cannot be blank' };
}

export function checkEmailValidation(val: any) {
  return !!RegExp(emailValidationRegex).exec(val);
}
//==================end email validation==============

// url validation==============================
export function validateURL(val: any, message?: any) {
  if (val === undefined) return null;
  else if (val.length > 0) {
    return checkURLValidation(val) ? null : { errorMessage: 'This url is not valid' };
  } else return { errorMessage: message || 'This field cannot be blank' };
}

export function checkURLValidation(val: any) {
  return !!RegExp(websiteURLRegex).exec(val);
}
//==================end url validation==============

export function validateRequired(val: any, field?: any, message?: any) {
  return val !== undefined && val?.length <= 0
    ? { errorMessage: message || 'This field cannot be blank' }
    : null;
}

export function roleNameRegex(val: any, message?: any) {
  const pattern = /^[a-zA-Z0-9 _@.-]*$/;

  if (val !== undefined)
    return RegExp(pattern).exec(val)
      ? null
      : {
          errorMessage:
            message ||
            'Role name allows only spaces, characters, numbers, and special characters such as -, _, @, and ..',
        };
  else return null;
}

export function alphaNumericOnlyWithSpace(e: any) {
  if (!alphaNumericValidationRegexWithSpace.test(e.key)) {
    e.preventDefault();
  }
}

export function alphaNumericOnly(e: any) {
  if (!alphaNumericValidationRegex.test(e.key)) {
    e.preventDefault();
  }
}

export function alphaNumericOnlyWithSpaceOnPaste(e: any) {
  const value = e?.clipboardData?.getData('Text');
  if (!alphaNumericValidationRegexWithSpace.test(value)) {
    e.preventDefault();
  }
}

export function alphaNumericOnlyOnPaste(e: any) {
  const value = e?.clipboardData?.getData('Text');
  if (!alphaNumericValidationRegex.test(value)) {
    e.preventDefault();
  }
}
