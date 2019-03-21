api_name = 'set_group_anonymous_ban'
class_name = ''.join([i.capitalize() for i in api_name.split('_')])+'Request'
api_path = '/'+api_name
base_class = 'ApiRequest'
params = [ 'long group_id', 'string flag', 'long duration' ]
summary = '群组匿名用户禁言'
is_empty = True

template = f'''
using cqhttp.Cyan.ApiCall.Requests.Base;

namespace cqhttp.Cyan.ApiCall.Requests {{
    /// <summary>
    /// {summary}
    /// </summary>
    public class {class_name} : {base_class} {{
        {';'.join(params)};
        ///
        public {class_name} ({','.join(params)}) : base ("{api_path}") {{
            this.response = new Results.{'Empty' if is_empty else f'{class_name[:-7]}'}Result();
            {';'.join(['this.{0} = {0}'.format(i.split(' ')[1]) for i in params])};
        }}
        ///
        public override string content {{
            get {{
                return $"{{{{'''+ \
                    ','.join(['~"'+i[1]+'~":'+('{'+i[1]+'}' if i[0]!='string' else f'~"{{Config.asJsonStringVariable({i[1]})}}~"') for i in [i.split(' ') for i in params]])+ \
f'''}}}}";
            }}
        }}
    }}
}}
'''
template = template.replace('~','\\')

print(template)
x = open(class_name+'.cs', 'w')
x.write(template)
x.close()
