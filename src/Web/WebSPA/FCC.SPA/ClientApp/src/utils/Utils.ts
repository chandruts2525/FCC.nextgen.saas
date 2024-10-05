export const encodeQuery = (params: any) => {
  let query = "";
  for (let d in params)
    query += encodeURIComponent(d) + "=" + encodeURIComponent(params[d]) + "&";
  return query.slice(0, -1);
};
