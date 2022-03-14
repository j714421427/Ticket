Vue.component('page', {
    props: ['gridhead', 'griddata'],
    template: `<div>
    <div class="grid">
        <div class="divTable">
            <div class="divTableBody">
                <div class="divTableRow">
                    <div v-for="item in gridhead" class="divTableCell">
                        <label >{{item}}</label>
                    </div>
                </div>
                <div v-for="item in griddata" class="divTableRow">
                    <div v-for="(key,index) in item" class="divTableCell">
                        <input v-if="gridhead[index] == 'Edit'" type="button" value="Edit" v-on:click="editEvent(key)" />
                        <input v-else-if="gridhead[index] == 'Delete'" type="button" value="Delete" v-on:click="deleteEvent(key)" />
                        <input v-else-if="gridhead[index] == 'Resolve'" type="button" value="Resolve" v-on:click="resolveEvent(key)" />
                        <input v-else-if="gridhead[index] == 'Detail'" type="button" value="Detail" v-on:click="detailEvent(key)" />
                        <label v-else>{{key}}</label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>`,
    methods: {
        editEvent: function (itemId) {
            this.$emit('edititem', itemId);
        },
        deleteEvent: function (itemId) {
            this.$emit('deleteitem', itemId);
        },
        resolveEvent: function (itemId) {
            this.$emit('resolveitem', itemId);
        },
        detailEvent: function (itemId) {
            this.$emit('detailitem', itemId);
        }
    }
});

Vue.component('ht-dropdown', {
    template: `
    <div class="ht-dropdown">
      <div class="ht-dropdown-status" @click="toggle" @mouseleave="close">
        <p class="ht-dropdown-status-text" :title="statusTitle">
          {{ statusText }}
        </p>

        <div v-show="isShow" class="ht-dropdown-options">
          <div
            v-for="(option, statusId) in options"
            v-show="option.isShow"
            @click.stop="setStatus(option, statusId)"
            class="ht-dropdown-option"
            :title="option.optionTitle"
          >
            <p>{{ option.optionText }}</p>
          </div>
        </div>
      </div>
      <p class="error-msg">{{ errorMsg }}</p>
    </div>
  `,
    props: {
        status: Number,
        options: Object,
        inititle: String,
    },
    data() {
        return {
            isShow: false,
            isError: false,
            errorMsg: ''
        }
    },
    computed: {
        statusText() {
            if (typeof (this.options[this.status]) != "undefined") {
                return this.options[this.status].statusText;
            }
            return this.inititle;
        },
        statusTitle() {
            if (typeof (this.options[this.status]) != "undefined") {
                return this.options[this.status].statusTitle;
            }
            return this.inititle;
        }
    },
    methods: {
        toggle() {
            this.isShow = !this.isShow;
        },
        close() {
            this.isShow = false;
        },
        setStatus(option, statusId) {
            this.close();
            this.$emit('updatehtdropdownstatus', statusId, this.setErrorMsg);
        },
        setErrorMsg(errorMsg) {
            this.errorMsg = errorMsg;
        }
    }
});