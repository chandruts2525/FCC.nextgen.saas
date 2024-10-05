import ApiConfig from '../../api/ApiConfig';

export async function callJobTypeApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

// export async function getJobTypesList(url: string, param: any, data: any) {
//   const response = await ApiConfig.post(url, data, {}, param);
//   return response;
// }

// export async function addJobType(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function editJobType(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function deleteJobType(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function exportAllJobTypes(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }
