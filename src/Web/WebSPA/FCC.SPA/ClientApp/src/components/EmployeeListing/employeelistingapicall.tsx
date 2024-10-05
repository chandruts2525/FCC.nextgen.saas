import ApiConfig from "../../api/ApiConfig";

export async function employeeAPI(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

// export async function addEmployee(url: string, data?: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function getEmployeeList(url: string, param: any, data: any) {
//   const response = await ApiConfig.post(url, data, {}, param);
//   return response;
// }

// export async function updateEmployee(url: string, data?: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }
