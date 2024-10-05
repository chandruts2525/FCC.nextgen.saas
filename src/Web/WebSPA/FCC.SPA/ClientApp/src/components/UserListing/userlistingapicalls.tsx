import ApiConfig from '../../api/ApiConfig';

// export async function getUserList(url:string, param:any){
//     const response=await ApiConfig.get(url, param)
//     return response
// }
export async function getUserDetails() {
  const response = await ApiConfig.get('/api/Auth/GetUser');
  return response;
}

export async function signoutUser() {
  const response = await ApiConfig.get('/api/Auth/Logout');
  return response;
}

export async function userAPI(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

// export async function getUserList(data: any) {
//   const response = await ApiConfig.post(data);
//   return response;
// }

// export async function addUser(data: any) {
//   const response = await ApiConfig.post(data);
//   return response;
// }

// export async function editUser(data: any) {
//   const response = await ApiConfig.post(data);
//   return response;
// }

export async function deleteUser(url: string) {
  const response = await ApiConfig.delete(url);
  return response?.data?.data;
}

// export async function fetchUserPermissionOptions(payload: any) {
//   const response = await ApiConfig.post(payload);
//   return response;
// }

// export async function fetchEmployeeOptions(payload: any) {
//   const response = await ApiConfig.post(payload);
//   return response;
// }

// export async function fetchUserById(payload: any) {
//   const response = await ApiConfig.post(payload);
//   return response;
// }

// export async function deactivateUser(payload: any) {
//   const response = await ApiConfig.post(payload);
//   return response;
// }
