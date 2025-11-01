## New Board Control Manager

Ares![img](http://szaresweb.ptn.gwkf.cn/Images/Menu/icon_menu.gif)Maintain![img](http://szaresweb.ptn.gwkf.cn/Images/Menu/icon_menu.gif)FT Modules![img](http://szaresweb.ptn.gwkf.cn/Images/Menu/icon_menu.gif)FT Product/Machine Release System![img](http://szaresweb.ptn.gwkf.cn/Images/Menu/icon_page.gif)New Board Control Manager

#### 1.功能描述

页面展示处于黑名单中的板，用户可以在此页面release板，否则使用此板生产时，会被PMRS Hold.

新客户别注册：用户在 Ares-Maintain-FT Modules-FT Product/Machine Release System-Device information可添加注册新客户别，目前添加完新客户别未主动添加板到ARES 板 BLACKLIST；

新板注册：有新的板子在TBIE被注册后，会直接被加入到BLACKLIST，用户可以在ARES页面释放板子到生产；

#### 2.系统流程

- TBIE页面：新板注册后，请求Portcmd方法：ins_tool_data；

- Portcmd中使用ecs1500.dll中的InsertToolsData方法，插入数据到epnc_file;

- 插入到epnc_file后，表内数据可以在New Board Control Manager页面展示，客户与板号为联合主键；

- 生产时，自动化请求Portcmd：PMRS_FT/PMRS_IR/PMRS_BI，Portcmd会联合enj_file与epnc_file联合查询，板子必须在enj表内，但是却不在epnc_file内；

- 新客户导入后，目前需工程提事件单将所有的板加入到PMRS管控，操作SQL:

  ```
  --删除之前的部分数据
  SELECT * FROM SZMESDB.EMSDB.DBO.epnc_file where epnc02 in ('AUSPGA')
  --DELETE FROM epnc_file where epnc02 in ('AUSPGA')
  
  --导入现有的所有板
  INSERT INTO dbo.epnc_file (epnc01, epnc02, epnc03)(
  	SELECT
  		GETDATE(),
  		'AUSPGA',
  		*
  	FROM
  		(
  			SELECT DISTINCT
  				enj06
  			FROM
  				enj_file
  			WHERE
  				enj03 = 'FT'
  			AND enj04 = 'BIB'
  		) AS A
  )
  ```

- 用户可以在ARES页面释放板，使用Delete和DeleteAll多选删除及全部删除；

​		![image-20220812085029820](./.assets/typora-user-images/image-20220812085029820.png)

