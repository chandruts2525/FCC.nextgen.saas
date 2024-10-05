import ApiConfig from '../../api/ApiConfig';

// export async function getCompanyList(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function getModuleList(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function getQuoteFooter(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function getQuoteFooterList(url: string, param: any, data: any) {
//   const response = await ApiConfig.post(url, data, {}, param);
//   return response;
// }

// export async function getQuoteFooterAttachment(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function addQuoteFooter(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function addAttachment(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function updateQuoteFooter(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function editQuoteFooter(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response?.data?.data;
// }

// export async function deleteQuoteFooter(url: string, data?: any, param?: any) {
//   const response = await ApiConfig.put(url, data, param);
//   return response;
// }

// export async function deleteAttachment(url: string, param?: any) {
//   const response = await ApiConfig.delete(url, param);
//   return response;
// }

// export async function exportQuoteFooter(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

// export async function deactivateQuoteFooter(url: string) {
//   const response = await ApiConfig.put(url);
//   return response;
// }

export async function callQuoteFooterApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}
