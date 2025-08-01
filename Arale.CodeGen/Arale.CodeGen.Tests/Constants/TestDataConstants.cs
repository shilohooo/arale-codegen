﻿namespace Arale.CodeGen.Tests.Constants;

/// <summary>
///     Constants of test data.
/// </summary>
public static class TestDataConstants
{
    public const string TestJsonStr = """
                                      [
                                        {
                                          "id": 1,
                                          "username": "shiloh",
                                          "gender": 1,
                                          "birthday": "1998-03-02",
                                          "email": null,
                                          "pi": 3.1415926,
                                          "address": {
                                            "country": "China",
                                            "province": "Guangdong",
                                            "city": "FoShan",
                                            "street": "桂城街道"
                                          },
                                          "doubleNumArr": [
                                            1.52,
                                            2.53,
                                            3.53,
                                            4.54,
                                            5.55
                                          ],
                                          "intArr": [
                                            1,
                                            2,
                                            3,
                                            4,
                                            5
                                          ],
                                          "strArr": [
                                            "a",
                                            "b",
                                            "c",
                                            "d",
                                            "e"
                                          ],
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
                                        },
                                        {
                                          "id": 2,
                                          "username": "bruce",
                                          "gender": 1,
                                          "birthday": "1998-03-02",
                                          "email": "bruce@gmail.com",
                                          "pi": 3.1415926,
                                          "address": {
                                            "country": "China",
                                            "province": "Guangdong",
                                            "city": "FoShan",
                                            "street": "桂城街道"
                                          },
                                          "enabled": false
                                        }
                                      ]
                                      """;
}
