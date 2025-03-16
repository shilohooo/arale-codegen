/**
 * 菜单信息
 * @author shiloh
 * @date 2024/7/16 14:17
 */

export interface Menu {
  path?: string
  name?: string
  label: string
  icon?: string
  level?: number
  component?: string
  type: MenuType
  iframeSrc?: string
  children?: Menu[]
}

export const enum MenuType {
  CATALOG = 0,
  PAGE = 1,
  LINK = 2,
}

export const HOME_MENU = {
  icon: 'home',
  name: 'home',
  label: 'Home',
  path: '/home',
  type: MenuType.PAGE,
}

/**
 * 菜单元信息
 *
 * @author shiloh
 * @date 2024/7/16 14:28
 */

export const menuList: Menu[] = [
  {
    icon: 'mdi-database-search',
    label: 'SQL Convert',
    type: MenuType.CATALOG,
    path: '/sql-convert',
    children: [
      {
        icon: 'mdi-language-csharp',
        label: 'SQL to Class',
        path: '/sql-convert/sql-to-class',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/sql-convert/SqlToClass',
      },
      {
        icon: 'mdi-language-java',
        label: 'SQL to Entity',
        path: '/sql-convert/sql-to-entity',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/sql-convert/SqlToEntity',
      },
    ],
  },
  {
    icon: 'mdi-code-json',
    label: 'JSON Convert',
    type: MenuType.CATALOG,
    path: '/json-convert',
    children: [
      {
        icon: 'mdi-language-typescript',
        label: 'JSON to TS',
        path: '/json-convert/json-to-ts',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/json-convert/JsonToTs',
      },
      {
        icon: 'mdi-language-csharp',
        label: 'JSON to Class',
        path: '/json-convert/json-to-class',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/json-convert/JsonToClass',
      },
      {
        icon: 'mdi-language-javascript',
        label: 'JSON to JS',
        path: '/json-convert/json-to-js',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/json-convert/JsonToJs',
      },
      {
        icon: 'mdi-code-json',
        label: 'JSON property case',
        path: '/json-convert/json-property-case',
        type: MenuType.PAGE,
        level: 1,
        component: 'modules/json-convert/JsonPropertyCase',
      },
    ],
  },
]

/**
 * 获取所有类型为 PAGE 的路由的 path
 * @param menus 菜单列表
 * @param flatMenus 结果
 * @author shiloh
 * @date 2025/1/25 9:50
 */
export function getFlatMenus(menus: Menu[], flatMenus: Menu[] = []) {
  for (const menu of menus) {
    if (menu.children?.length) {
      getFlatMenus(menu.children, flatMenus)
    }

    flatMenus.push(menu)
  }

  return flatMenus
}
