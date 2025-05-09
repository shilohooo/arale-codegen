/**
 * @author shiloh
 * @date 2025/2/16 10:45
 */

// JSON object to test
export const JSON_OBJECT_TEST_STR = `
{
  "id": 1,
  "username": "shiloh",
  "gender": 1,
  "birthday": "1998-03-02",
  "createTime": "2025-02-27 10:00:05",
  "email": null,
  "pi": 3.1415926,
  "address": {
    "country": "China",
    "province": "Guangdong",
    "city": "FoShan",
    "street": "桂城街道"
  },
  "doubleNumArr": [1.52, 2.53, 3.53, 4.54, 5.55],
  "intArr": [1, 2, 3, 4, 5],
  "strArr": ["a", "b", "c", "d", "e"],
  "roles": [
    {
      "id": 1,
      "roleName": "admin",
      "permissions": [
        {
          "id": 1,
          "code": "sys:user:view",
          "description": "查看用户列表",
          "url": "/api/users"
        }
      ]
    },
    {
      "id": 2,
      "roleName": "user"
    }
  ],
  "enabled": true
}
`.trim()
