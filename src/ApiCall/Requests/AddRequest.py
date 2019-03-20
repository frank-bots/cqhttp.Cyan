api_name = 'get_image'
class_name = ''.join([i.capitalize() for i in api_name.split('_')])+'Request'
api_path = '/'+api_name
base_class = 'ApiRequest'
params = ['string filename']
summary = '获取图片路径'

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
            this.response = new Results.{class_name[:-7]}Result();
            {';'.join(['this.{0} = {0}'.format(i.split(' ')[1]) for i in params])};
        }}
        ///
        public override string content {{
            get {{
                return $"{{{{{','.join(['{1}"{0}{1}"={{{0}}}'.format(i.split(' ')[1],'~')for i in params])}}}}}";
            }}
        }}
    }}
}}
'''.replace('~', '\\')
x = open(class_name+'.cs', 'w')
x.write(template)
x.close()
