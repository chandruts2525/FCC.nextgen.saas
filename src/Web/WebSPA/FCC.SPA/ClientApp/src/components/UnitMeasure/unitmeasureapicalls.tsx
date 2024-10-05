import ApiConfig from '../../api/ApiConfig';

export async function unitOfMeasureAPI(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

// export async function getUnitOfMeasureList(url: string, param: any, data: any) {
//   const response = await ApiConfig.post(url, data, {}, param);
//   return response;
// }

// export async function addUnitOfMeasure(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function fetchUnitOfMeasureById(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

// export async function editUnitOfMeasure(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function deleteUser(url: string) {
//   const response = await ApiConfig.delete(url);
//   return response?.data?.data;
// }

// export async function fetchMeasureTypeOptions(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

// export async function deactivateUoM(url: string) {
//   const response = await ApiConfig.put(url);
//   return response;
// }
