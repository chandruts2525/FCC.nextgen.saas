import ApiConfig from '../../api/ApiConfig';

export async function callScheduleTypeApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

// export async function getScheduleTypesList(url: string, param: any, data: any) {
//   const response = await ApiConfig.post(url, data, {}, param);
//   return response;
// }

// export async function addScheduleType(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function editScheduleType(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function deleteScheduleType(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function exportAllScheduleTypes(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

// export async function getUnitOrComponentList(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

// export async function deactivateScheduleType(url: string) {
//   const response = await ApiConfig.put(url);
//   return response;
// }
