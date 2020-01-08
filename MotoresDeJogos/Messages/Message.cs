using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoresDeJogos
{

/*
Classe base que define uma mensagem de forma abstrata, ou seja, guarda as propriedades que erão comuns a todos os tipos de mensagens que poderemos vir a ter. Isto é importante para podermos ter listas de objetos do tipo "Mensagem", independentemente das características próprias de cada tipo de mensagem concreto. Para esta classe bastam duas propriedades (que todas as mensagens, independentemente do tipo, têm em comum) e um construtor:
- propriedade do tipo MessageType (tipo de mensagem definido pelo enum acima)
- propriedade booleana que indica se a mensagem já foi lida ou não
- construtor que define o valor da propriedade MessageType (que recebe por parametro) e define a propriedade booleana como false
*/
    class Message
    {

        private MessageType messageType;

        public MessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

        private Boolean read;

        public Boolean Read
        {
            get { return read; }
            set { read = value; }
        }

        public Message(MessageType messageType)
        {
            this.messageType = messageType;
            this.read = false;
        }
    }
}
